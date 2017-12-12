using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using MovieApp.Parse.Models;
using MvvmHelpers;
using Parse;
using Xamarin.Forms;

namespace MovieApp.Parse.ViewModel
{
    public class MoviesViewModel : BaseViewModel
    {
        private string _name;

        private string _nameAuthor;

        private string _nameMovie;

        private ObservableCollection<Movies> _listMovies;
        
        public MoviesViewModel()
        {
            Save = new Command<Movies>(InvokeSaveAsync);

            GetData = new Command(InvokeGetData);

        }

        public Command<Movies> Save { get; }

        public Command GetData { get; }   

        public string Name { get => _name; set => SetProperty(ref _name, value); }

        public ObservableCollection<Movies> ListMovies 
        { 
            get
            {
               return  _listMovies ?? ( _listMovies = new ObservableCollection<Movies>());
            }
            set => SetProperty(ref _listMovies, value); 
        }

        public string NameAuthor { get => _nameAuthor; set => SetProperty(ref _nameAuthor, value); }

        public string NameMovie { get => _nameMovie; set => SetProperty(ref _nameMovie, value); }

        public bool IsUpdate { get; set; }

        public async void InvokeSaveAsync(Movies movieItem = null)
        {
            try
            {
                Movies movie = new Movies();
                movie.Author = NameAuthor;
                movie.Name = NameMovie;

                if(IsUpdate)
                {
                    movie.ObjectId = movieItem.ObjectId;

                    if (await ServiceClient.Instance.SaveAsync(movie))
                    {
                        await Application.Current.MainPage.DisplayAlert("Good", "the movie was updated", "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "the movie was not updated", "OK");
                    }
                }
                else
                {
                    if (await ServiceClient.Instance.SaveAsync(movie))
                    {
                        await Application.Current.MainPage.DisplayAlert("Good", "the movie was saved", "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "the movie was not saved", "OK");
                    }
                }


                NameAuthor = "";
                NameMovie = "";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error in InvokeSaveAsync method: {0}", ex.Message));
            }
        }

        public async void Delete(Movies objectEntity)
        {
            try
            {
                await ServiceClient.Instance.DeleteAsync(objectEntity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error in InvokeSaveAsync method: {0}", ex.Message));
            }
        }

        public async void InvokeGetData()
        {
            try
            {
                ListMovies.Clear();
                // to get all the collection form specific table.
                var test1 = await ServiceClient.Instance.GetAsync<Movies>();

                //to get the object form a specific table by id.
                var test2 = await ServiceClient.Instance.GetAsync<Movies>("Q3bRCXBTki");

                // to create a query and get a IEnumerable from the specific table
                var query = new ParseQuery<Movies>().WhereEqualTo("Name", "Fernando");
                var test3 = await ServiceClient.Instance.GetAsync<Movies>(query);

                foreach(Movies movie in test1)
                {
                    ListMovies.Add(movie);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error in InvokeGetData method: {0}", ex.Message));
            }
        }
    }
}
