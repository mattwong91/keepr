
namespace keepr.Repositories;

public class ProfilesRepository
{
  private readonly IDbConnection _db;

  public ProfilesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Profile EditProfile(Profile profiletoUpdate)
  {
    string sql = @"
    UPDATE accounts
    SET
    name = @Name,
    picture = @Picture,
    coverImg = @CoverImg
    WHERE id = @Id LIMIT 1;

    SELECT * FROM accounts WHERE id = @Id;";

    Profile profile = _db.Query<Profile>(sql, profiletoUpdate).FirstOrDefault();
    return profile;
  }

  internal Profile GetProfileById(string profileId)
  {
    string sql = "SELECT * FROM accounts WHERE id = @profileId;";

    Profile profile = _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
    return profile;
  }
}