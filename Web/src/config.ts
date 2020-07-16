export default Object.freeze({
    endpoints: {
        convert: "https://localhost:44332/api/image",
        downloadResult: "https://localhost:44332/api/result/"
    },
    inputFormats: "image/jpeg, image/svg+xml, application/dicom, .dcm",
    outputFormats: ["PDF", "PSD", "PNG"],
})