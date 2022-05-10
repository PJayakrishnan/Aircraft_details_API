using AircraftWebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AircraftWebAPI.Controllers
{
    /// <summary>
    /// The AircraftsController will deal with all the informations about Aircrafts.
    /// </summary>
    public class AircraftsController : ApiController
    {
        List<AircraftDetails> aircraftObjList = new List<AircraftDetails>();

        /// <summary>
        /// AircraftsController constructor where the data from json file is fetched and deserialized
        /// </summary>
        public AircraftsController()
        {
            string allText = File.ReadAllText(HttpContext.Current.Server.MapPath("~/API_Data/AircraftDetails.json"));
            aircraftObjList = JsonConvert.DeserializeObject<List<AircraftDetails>>(allText);
        }


        /// <summary>
        /// Gets all the information about all the aircrafts
        /// </summary>
        /// <returns>List of all the aircrafts</returns>
        // GET api/Aircrafts
        public List<AircraftDetails> Get()
        {
            return aircraftObjList;
        }


        /// <summary>
        /// Gets the informations about the aircraft you searched for.
        /// </summary>
        /// <param name="searchWord">Search term entered for searching</param>
        /// <returns>List of aircrafts matching your search term</returns>
        [HttpGet]
        [Route("api/Aircrafts/{searchWord}")]
        // GET api/Aircrafts/searchWord
        public List<AircraftDetails> Get(string searchWord)
        {
            List<AircraftDetails> SearchRsltList = new List<AircraftDetails>();

            for (int i=0; i < aircraftObjList.Count; i++)
            {
                for(int j = 0; j < aircraftObjList[i].searchTags.Count; j++)
                {
                    if(aircraftObjList[i].Aircraft == searchWord || aircraftObjList[i].searchTags[j] == searchWord)
                    {
                        SearchRsltList.Add(aircraftObjList[i]);
                        goto jumpSpot;
                    }
                }
                jumpSpot:
                bool True = true;
            }

            return SearchRsltList;
        }

    }
}