using System;
using Entith.AspNet.Domain;

namespace Entith.AspNet.ModuledController
{
	public class DomainTimestampControllerModule : ControllerModule
    {
		#pragma warning disable 0414
		private TimestampableService _service;
		#pragma warning restore 0414

        public DomainTimestampControllerModule(TimestampableService service)
        {
			// Hold on to service instance so timestamp logic can be called.
			// Not sure if this is necessary
			_service = service;
        }
    }
}

