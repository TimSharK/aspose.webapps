using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Aspose.Imaging.CoreExceptions;
using Domain;
using Domain.Exceptions;
using Moq;
using NUnit.Framework;
using Services;

namespace Tests
{
    public class AsposeImageConverterServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UnknownFormat_ThrowsConvertException()
        {
            var resultStorageMock = new Mock<IResultStorage>();
            var service = new AsposeImageConverterService(resultStorageMock.Object);
            Assert.ThrowsAsync<ConvertException>(() => service.ConvertAsync(new byte[256], "UNKNOWN"));
        }

        [Test]
        public void WrongFile_ThrowsImageLoadException()
        {
            var resultStorageMock = new Mock<IResultStorage>();
            var service = new AsposeImageConverterService(resultStorageMock.Object);
            Assert.ThrowsAsync<ImageLoadException>(() => service.ConvertAsync(new byte[256], "PDF"));
        }

        [TestCaseSource(nameof(GetConvertPairs))]
        public async Task ConvertTest(Tuple<string, string> convertPair)
        {
            var testId = Guid.NewGuid().ToString();
            var resultStorageMock = new Mock<IResultStorage>();

            resultStorageMock.Setup(x => x.SaveResultAsync(It.IsAny<Stream>()))
                .ReturnsAsync(testId);

            var service = new AsposeImageConverterService(resultStorageMock.Object);

            
            using var inputStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(convertPair.Item1);
            using var memoryStream = new MemoryStream();
            inputStream.CopyTo(memoryStream);

            var id = await service.ConvertAsync(memoryStream.ToArray(), convertPair.Item2);

            Assert.AreEqual(testId, id);
        }

        public static IEnumerable<Tuple<string, string>> GetConvertPairs()
        {
            var formats = new[] {"PDF", "PSD", "PNG"};
            var files = new List<string>();

            var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            foreach (var resourceName in names)
            {
                if (!resourceName.StartsWith("Tests.Assets.Convertibles"))
                    continue;

                files.Add(resourceName);
            }

            foreach (var file in files)
            {
                foreach (var format in formats)
                {
                    yield return new Tuple<string, string>(file, format);
                }
            }
        }
    }
}