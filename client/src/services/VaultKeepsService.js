import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { VaultKeep } from "../models/VaultKeep"
import { api } from "./AxiosService"

class VaultKeepsService {

  async getKeepsInVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)

    AppState.keeps = res.data.map(obj => new Keep(obj))
  }

}

export const vaultKeepsService = new VaultKeepsService()