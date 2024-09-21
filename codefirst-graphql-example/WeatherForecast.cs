namespace codefirst_graphql_example
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }

    public class Book
    {
        public string Title { get; set; }

        public Author Author { get; set; }

        public string Genre { get; set; }
    }

    public class Author
    {
        public string Name { get; set; }
    }

    public class BookQuery
    {
        public Book GetBook() =>
            new Book
            {
                Title = "C# in depth.",
                Author = new Author
                {
                    Name = "Jon Skeet"
                }, 
                Genre = "IT"
            };
    }

    public class EntirelyDifferentFunction
    {
        public Book GetBook() =>
            new Book
            {
                Title = "C# in depth.",
                Author = new Author
                {
                    Name = "Jon Skeet"
                },
                Genre = "IT"
            };
    }

    public class QueryType : ObjectType<BookQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<BookQuery> descriptor)
        {
            descriptor
                .Field(f => f.GetBook())
                .Type<BookType>();
        }
    }

    public class ExtQueryType : ObjectType<EntirelyDifferentFunction>
    {
        protected override void Configure(IObjectTypeDescriptor<EntirelyDifferentFunction> descriptor)
        {
            descriptor
                .Field(f => f.GetBook())
                .Type<ExtBookType>();
        }
    }

    public class BookType : ObjectType<Book>
    {
        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {            
            descriptor
                .Field(f => f.Title)
                .Type<StringType>();

            descriptor
                .Field(f => f.Author.Name)
                .Type<StringType>();

        }
    }
           
    public class ExtBookType : ObjectType<Book>
    {
        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {
          
            descriptor
                .Field(f => f.Title)
                .Type<StringType>();

            descriptor
                .Field(f => f.Author.Name)
                .Type<StringType>();

            descriptor
            .Field("genres")
            .Type<StringType>()
            .Resolve(context =>
            {
                var parent = context.Parent<Book>();
                return "helllloooo";                
            });

        }
    }
}
