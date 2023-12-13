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

  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    logger.log("[GOT VAULT BY ID]", res.data)

    const newVault = new Vault(res.data)
    AppState.activeVault = newVault
  }

  async createVault(vaultData) {
    const res = await api.post('api/vaults', vaultData)
    logger.log("[CREATED VAULT]", res.data)

    return new Vault(res.data)
  }

  async deleteVaultById(vaultId) {
    await api.delete(`api/vaults/${vaultId}`)

    AppState.vaults = AppState.vaults.filter(vault => vault.id != vaultId)
  }

  addVault(newVault) {
    AppState.vaults.push(newVault)
  }

  clearVaults() {
    AppState.vaults = []
  }

  clearActiveVault() {
    AppState.activeVault = null
  }
}

export const vaultsService = new VaultsService()