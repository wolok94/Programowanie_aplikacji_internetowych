using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programowanie_aplikacji_internetowych.domain.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public string Test { get; set; }
    public MetaData MetaData { get; set; }
    public Guid MetaDataId { get; set; }
    public Post Post { get; set; }
    public Guid PostId { get; set; }
}
