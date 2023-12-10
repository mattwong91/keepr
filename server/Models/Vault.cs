namespace keepr.Models;

public class Vault : RepoItem<int>
{
  public string CreatorId { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public bool? IsPrivate { get; set; }
  public Profile Creator { get; set; }
}