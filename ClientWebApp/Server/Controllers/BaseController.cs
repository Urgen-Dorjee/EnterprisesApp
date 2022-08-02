namespace ClientWebApp.Server.Controllers
{
    public class BaseController : ControllerBase
    {
        public IMediator _Mediator;

        public BaseController(IMediator mediator)
        {
            _Mediator = mediator;
        }
    }
}
