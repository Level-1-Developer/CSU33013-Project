using System.Text.Json;
using System.Text.Json.Serialization;

namespace web_app.Models
{
    public class ActionItem
    {
        public int ID;
        public String target;
        public String status;
        public String campaign;
        public DateTime expiry;
        public DateTime when_created;
        public DateTime when_updated;
        public String content;
        public String country;
        public String language;
        public String customerset;
        public ActionItem(int ID, String target, String status, String campaign, String expiry, String when_created, String when_updated, String content, String country, String language, String customerset)
        {
            this.ID = ID;
            this.target = target;
            this.status = status;
            this.campaign = campaign;
            this.expiry = DateTime.Parse(expiry);
            this.when_created = DateTime.Parse(when_created);
            this.when_updated = DateTime.Parse(when_updated);
            this.content = content;
            this.country = country;
            this.language = language;
            this.customerset = customerset;
        }
    }

}
