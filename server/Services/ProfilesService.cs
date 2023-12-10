namespace keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _profilesRepo;

  public ProfilesService(ProfilesRepository profilesRepo)
  {
    _profilesRepo = profilesRepo;
  }

  internal Profile GetProfileById(string profileId)
  {
    Profile profile = _profilesRepo.GetProfileById(profileId);
    return profile;
  }
}