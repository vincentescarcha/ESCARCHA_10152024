namespace FileProcessor
{
    public static class Constants
    {
        public static class Messages
        {
            public const string UnexpectedError = "An unexpected error occurred.";
            public const string ApiKeyGenerationError = "An error occurred while generating the API key.";
            public const string ApiKeyMissing = "An error occurred while generating the API key.";

            public const string NoFileUploaded = "No file uploaded.";
            public const string FileUploadedError = "Error occurred while processing the file.";
            public const string UnsupportedFileType = "Unsupported file type"; 
            public const string InvalidCsvQuery = "Invalid csv query.Expected format: aggregate=<column_number>";
            public const string NoQueryProvided = "No query provided.";
        }

    }
}
