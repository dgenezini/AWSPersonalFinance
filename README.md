# Architecture

<img align="left" alt="Project Architecture" title="Project Architecture" src="https://raw.githubusercontent.com/dgenezini/AWSPersonalFinance/main/PersonalFinance.png?token=ACRTFQ4JLJQBYGCYMSY7XSLAZSDJU" />

# CI/CD

# Technologies

- .NET 5.0 + Blazor WebAssembly (Single Page App)
- .NET 3.1 + ASP.NET API on AWS Lambda
- .NET 3.1 Console App on AWS Lambda
- AWS API Gateway
- DynamoDB
- DynamoDB Streams
- SNS
- Cloudfront + SAM
- Cognito
- Cloudformation
- CodeArtifact
- CodePipeline + CodeBuild + CodeDeploy
- S3 Static Site

# Parts

- [Frontend](https://github.com/dgenezini/PersonalFinance.Frontend)
- [API](https://github.com/dgenezini/PersonalFinance.API)
- [ImportService](https://github.com/dgenezini/PersonalFinance.ImportService)
- [Aggregator](https://github.com/dgenezini/PersonalFinance.Aggregator)
- [ServiceCommon](https://github.com/dgenezini/PersonalFinance.ServiceCommon)
