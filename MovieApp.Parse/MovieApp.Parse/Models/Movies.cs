using System;
using Parse;

namespace MovieApp.Parse.Models
{
    [ParseClassName("Movies")]
    public class Movies : ParseObject
    {
        [ParseFieldName("Author")]
        public string Author 
        { 
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        [ParseFieldName("Name")]
        public string Name
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
    }
}
