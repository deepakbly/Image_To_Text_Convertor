using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace imgtotext.Models
{
    public class ImageUploadModel
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}