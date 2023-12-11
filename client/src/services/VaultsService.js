import { AppState } from "../AppState"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {
  async getVaultsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log("[GOT VAULTS FOR PROFILE", res.data)

    AppState.vaults = res.data.map(obj => new Vault(obj))
  }
}

export const vaultsService = new VaultsService()