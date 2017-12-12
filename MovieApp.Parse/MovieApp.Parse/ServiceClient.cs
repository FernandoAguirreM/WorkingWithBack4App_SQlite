using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using MovieApp.Parse.Models;
using Newtonsoft.Json;
using Parse;
using System.Linq;

namespace MovieApp.Parse
{
    public class ServiceClient
    {
        private const string BASE_URL = "https://parseapi.back4app.com";

        private const string APPLICATION_ID = "1AfpH2IGqePsLZna8QhE629v647kWHCdc9jxTqpS";

        private const string NET_KEY = "1BcsVfEf8jX6ZDCGF6HazSH5ECZqsXJf5B7fYg2E";


        public ServiceClient()
        {
        }

        /// <summary>
        /// The instance.
        /// </summary>
        static readonly Lazy<ServiceClient> instance = new Lazy<ServiceClient>(() => new ServiceClient());

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static ServiceClient Instance => instance.Value;

        /// <summary>
        /// Init this instance.
        /// </summary>
        public void Init()
        {
            try
            {
                //You need to register the models that you want to use with parse. this always need to be before initialize the client.
                ParseObject.RegisterSubclass<Movies>();

                ParseClient.Initialize(new ParseClient.Configuration
                {
                    ApplicationId = APPLICATION_ID,
                    WindowsKey = NET_KEY,
                    Server = BASE_URL
                });

                ParseInstallation.CurrentInstallation.SaveAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error in ServiceClient class ==> Init method: {0}", ex.Message));
            }
        }

        /// <summary>
        /// Gets the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public async Task<IEnumerable<T>> GetAsync<T>() where T : ParseObject
        {
            try
            {
                var query = new ParseQuery<T>();

                return await query.FindAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error in ServiceClient class ==> GetAsync method: {0}", ex.Message));

                return new List<T>();
            }
        }

        /// <summary>
        /// Gets the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="objectId">Object identifier.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public async Task<T> GetAsync<T>(string objectId) where T : ParseObject
        {
            IEnumerable<T> result = new List<T>();
            try
            {
                var query = new ParseQuery<T>().Where(x => x.ObjectId == objectId);

                return (await query.FindAsync()).FirstOrDefault<T>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error in ServiceClient class ==> GetAsync method: {0}", ex.Message));

                return result.FirstOrDefault<T>();
            }
        }

        /// <summary>
        /// Gets the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="query">Query.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public async Task<IEnumerable<T>> GetAsync<T>(ParseQuery<T> query) where T : ParseObject
        {
            IEnumerable<T> result = new List<T>();
            try
            {
                return await query.FindAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error in ServiceClient class ==> GetAsync method: {0}", ex.Message));

                return result;
            }
        }

        /// <summary>
        /// Saves the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="entity">Entity.</param>
        public async Task<bool> SaveAsync(ParseObject entity)
        {
            try
            {
                await entity.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error in ServiceClient class ==> SaveData method: {0}", ex.Message));
                return false;
            }
        }

        /// <summary>
        /// Deletes the async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="entity">Entity.</param>
        public async Task<bool> DeleteAsync(ParseObject entity)
        {
            try
            {
                await entity.DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Error in ServiceClient class ==> DeleteAsync method: {0}", ex.Message));
                return false;
            }
        }
    }
}
