using System.Web;

namespace NHH.Models.Official
{
    /// <summary>
    /// handler基类
    /// </summary>
    public abstract class Handler
    {
        protected Handler(HttpContext context)
        {
            Request = context.Request;
            Response = context.Response;
            Context = context;
            Server = context.Server;
        }

        public abstract void Process();

        public HttpRequest Request { get; private set; }
        public HttpResponse Response { get; private set; }
        public HttpContext Context { get; private set; }
        public HttpServerUtility Server { get; private set; }
    }
}
