using BootSharp.Business.Interfaces;

namespace BootSharp.Business
{
    public abstract class ServiceBase : IService
    {
        private string _name;
        private string _description;

        protected ServiceBase(string name = null, string description = null)
        {
            _name = string.IsNullOrWhiteSpace(name) ? GetType().FullName : name;
            _description = description;
        }

        public string Description { get { return _description; } }
        public string Name {  get { return _name; } }

        public virtual void Dispose()
        {

        }
    }
}
