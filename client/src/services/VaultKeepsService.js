import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {

  async getKeepsInVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log("[GOT KEEPS IN VAULT]", res.data)
    AppState.keeps = res.data.map(obj => new Keep(obj))
  }

  async addKeepToVault(vaultKeepData) {
    const res = await api.post('api/vaultkeeps', vaultKeepData)
    logger.log("[ADDED KEEP TO VAULT]", res.data)

  }

  async removeKeepFromVault(vaultKeepId) {
    await api.delete(`api/vaultkeeps/${vaultKeepId}`)

    AppState.keeps = AppState.keeps.filter(keep => keep.vaultKeepId != vaultKeepId)
  }
}

export const vaultKeepsService = new VaultKeepsService()