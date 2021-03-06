using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FleetPro2019.DAL;
using FleetPro2019.Models;

namespace FleetPro2019.Controllers
{
    [RoutePrefix("Server")]
    public class ServerController : ApiController
    {
        SERVERDBEntities sdb = new SERVERDBEntities();

        [Route("List/{_Code}")]
        public IHttpActionResult GetServerList(string _Code)
        {
            using (sdb)
            {
                var result = (from s in sdb.WebServers
                              where s.code == _Code
                              select new Server()
                              {
                                  Code = s.code,
                                  ConfigurationName = s.config_name,
                                  Name = s.name,
                                  Url = s.url,
                                  DefaultCoordinates = s.default_coordinates,
                                  ImagesUrl = s.images_url
                              }).ToList();
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("No servers available");
                }
            }
        }
    }
}
