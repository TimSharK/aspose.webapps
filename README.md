# About
This is the solution of the test task for Aspose to apply on vacation Aspose Web Application
Author - Timur Gimranov

# Design
This is ASP.NET Core 3.1 API application with Vue.JS frontend that helps to convert JPG, SVG, DICOM files to PDF, PSD, PNG formats.
All results are stored in file system (Api/results) now. To store them anywhere else you need to implement and register additional IResultStorage interface.
API can be scaled via Docker and some load balancer. But in case of using a proper implementation of IResultStorage (like for Amazon S3).
The weak side of the solution is that some negative scenarios are not handled properly due to limitiation of time I can spend on each not-paid test task.
This design for huge load can be improved by using the message bus to store all convert tasks which will be handled by independent parallel instances of API.
The frontend then must use some sort of web sockets to listen on task completion;

# How to start
1. Start Api (cd Api) project with visual studio or dotnet run
2. Setup Front project (cd front)
2.1 Run 'npm install' or 'yarn install'
2.2 Adjust src/config.ts to specify your addresses to API
2.3 Run 'npm run serve' or 'yarn serve' to start the project

# Free libraries used
## API:
- Aspose.Imaging 20.6.0 (Trial)
- NUnit 3.12.0
- Moq 4.14.5
## Frontend:
- VueJS 2.6.11
- Vee Validate 3.3.7
- FilePond 4.18.0
