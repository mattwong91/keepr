namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _profilesService;
  private readonly KeepsService _keepsService;
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth0;

  public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService, Auth0Provider auth0)
  {
    _profilesService = profilesService;
    _keepsService = keepsService;
    _vaultsService = vaultsService;
    _auth0 = auth0;
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfileById(string profileId)
  {
    try
    {
      Profile profile = _profilesService.GetProfileById(profileId);
      return Ok(profile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
  {
    try
    {
      List<Keep> keeps = _keepsService.GetKeepsByProfileId(profileId);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]
  public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string profileId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _vaultsService.GetVaultsByProfileId(profileId, userInfo);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPut("{profileId}")]
  public async Task<ActionResult<Profile>> EditProfile(string profileId, [FromBody] Profile profileData)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      string userId = userInfo?.Id;
      Profile profile = _profilesService.EditProfile(profileId, userId, profileData);
      return Ok(profile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}