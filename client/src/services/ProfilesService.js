import { AppState } from "../AppState";
import { Profile } from "../models/Profile";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class ProfilesService {
  async getProfileById(profileId) {
    const res = await api.get(`api/profiles/${profileId}`)
    logger.log("[GOT PROFILE]", res.data)

    AppState.activeProfile = new Profile(res.data)
  }

  async editProfile(profileId, profileData) {
    const res = await api.put(`api/profiles/${profileId}`, profileData)
    logger.log("[UPDATED PROFILE]", res.data)

    AppState.activeProfile = new Profile(res.data)
  }

  clearActiveProfile() {
    AppState.activeProfile = null;
  }
}

export const profilesService = new ProfilesService()