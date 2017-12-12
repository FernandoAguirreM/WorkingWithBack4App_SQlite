using System;
using MovieApp.Parse.Models;
using Parse;
using Xamarin.Forms;

namespace MovieApp.Parse
{
    public partial class MovieApp_ParsePage : ContentPage
    {
        public MovieApp_ParsePage()
        {
            InitializeComponent();
        }

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var result = await DisplayActionSheet("Do you want to remove this movie", "Cancel",null,"Edit", "Delete");

            if(result == "Delete")
            {
                Movies movie = e.SelectedItem as Movies;
                vmMovies.Delete(movie);
                vmMovies.ListMovies.Remove(movie);
            }
            //if (result == "Edit")
            //{
            //    Movies movie = e.SelectedItem as Movies;
            //    vmMovies.NameAuthor = movie.Author;
            //    vmMovies.NameMovie = movie.Name;
            //    vmMovies.IsUpdate = true;
            //}
        }
    }
}
