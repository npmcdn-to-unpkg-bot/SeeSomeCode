﻿using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace SeeSomeCode
{
    public abstract partial class BaseApiController : ApiController
    {
        public BaseApiController() { /* parmless not allowed */ }

        protected HttpResponseMessage MakeResponse()
        {
            var hrm = new HttpResponseMessage( ModelState.IsValid ? HttpStatusCode.Accepted : HttpStatusCode.InternalServerError )
            {
                ReasonPhrase = "hello tom"
            };

            return hrm;
        }
    }

    [RoutePrefix( "api/values" )]
    public partial class ValuesController : BaseApiController
    {
        public ValuesController(int notUsed)
        {
            return;
        }

        [Route("")]
        public IEnumerable<GetDTO> Get()
        {
            DebugMessage("handling get request");

            return new GetDTO[] { new GetDTO() };
        }

        [Route( "{id}"  )]
        public GetDTO Get( int id )
        {
            DebugMessage(string.Format( "handling get request for [{0}]", id ));

            return new GetDTO();
        }

        [Route( "" )]
        public HttpResponseMessage Post( [FromBody] PostDTO postValue )
        {
            DebugMessage("handling post request");

            if( ModelState.IsValid )
            {
                // do something
            }
            return base.MakeResponse();
        }

        public class GetDTO
        {
            public int DtoId { get; set; } = 100;
            public string DtoName { get; set; } = "onetwothree";
        }

        public class PostDTO
        {
            [DictionaryElement("DtoId")]
            public int DtoId { get; set; } = 100;

            [DictionaryElement("FooName")]
            public string DtoName { get; set; } = "onetwothree";
        }
    }
}