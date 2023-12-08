namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vaultKeepsService;
  private readonly Auth0Provider _auth0;

  public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0)
  {
    _vaultKeepsService = vaultKeepsService;
    _auth0 = auth0;
  }
}