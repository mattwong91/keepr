import { VaultKeep } from "../models/VaultKeep"
import { api } from "./AxiosService"

class VaultKeepsService {

  async getKeepsInVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    const vaultKeeps = res.data.map(obj => new VaultKeep(obj))
    // TODO explore if I should return/join keep data to the vaultKeep on the backend?
  }

}

export const vaultKeepsService = new VaultKeepsService()