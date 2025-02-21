using Microsoft.OpenApi.Models;

namespace Flashcards.Api;

public static class StaticSwaggerData
{
    public static readonly string Version = "v1";
    public static readonly string Title = "Flashcards API";

    public static readonly string Description = """
                                                    A RESTful API built with ASP.NET Core for managing digital flashcards. This API provides endpoints for creating, retrieving, updating, and managing flashcards for learning purposes.
                                                
                                                    Key Features
                                                    * Lightweight API implementation
                                                    * PostgreSQL database integration
                                                    * Docker containerization support
                                                    * Automatic timestamp tracking for all entities
                                                    
                                                    Data Model
                                                    Each flashcard contains:
                                                    * Unique identifier
                                                    * Question text
                                                    * Answer text
                                                    * Deck association
                                                    * Creation and update timestamps
                                                    
                                                    For deployment details and complete source code, visit the GitHub repository.
                                                """;

    public static readonly OpenApiContact Contact = new OpenApiContact()
    {
        Name = "bluka1",
        Url = new Uri("https://github.com/bluka1")
    };
    public static readonly OpenApiLicense License = new OpenApiLicense()
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        };
}