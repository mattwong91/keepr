
namespace keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _profilesRepo;

  public ProfilesService(ProfilesRepository profilesRepo)
  {
    _profilesRepo = profilesRepo;
  }

  internal Profile EditProfile(string profileId, string userId, Profile profileData)
  {
    Profile profiletoUpdate = GetProfileById(profileId);
    if (profiletoUpdate.Id != userId)
    {
      throw new Exception("Cannot edit another users profile.");
    }

    profiletoUpdate.Name = profileData.Name ?? profiletoUpdate.Name;
    profiletoUpdate.Picture = profileData.Picture ?? profiletoUpdate.Picture;
    profiletoUpdate.CoverImg = profileData.CoverImg ?? profiletoUpdate.CoverImg;

    Profile profile = _profilesRepo.EditProfile(profiletoUpdate);
    return profile;
  }

  internal Profile GetProfileById(string profileId)
  {
    Profile profile = _profilesRepo.GetProfileById(profileId);
    if (profile == null)
    {
      throw new Exception($"Profile id: {profileId} is invalid");
    }
    return profile;
  }
}