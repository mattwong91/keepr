import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class KeepsService {
  async getAllKeeps() {
    const res = await api.get("api/keeps")
    logger.log("[GOT ALL KEEPS]", res.data)

    AppState.keeps = res.data.map(obj => new Keep(obj))
  }

  async getKeepById(keepId) {
    const res = await api.get(`api/keeps/${keepId}`)
    logger.log("[GET KEEP BY ID]", res.data)

    AppState.activeKeep = new Keep(res.data)
  }

  async getKeepsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`);
    logger.log("[GOT KEEPS FOR PROFILE", res.data);

    AppState.keeps = res.data.map(obj => new Keep(obj));
  }

  async createKeep(keepData) {
    const res = await api.post("api/keeps", keepData)
    logger.log("[CREATED KEEP]", res.data)

    return new Keep(res.data)
  }

  async deleteKeepById(keepId) {
    await api.delete(`api/keeps/${keepId}`)

    AppState.keeps = AppState.keeps.filter(keep => keep.id != keepId)
  }

  addKeep(keep) {
    AppState.keeps.push(keep)
  }

  clearActiveKeep() {
    AppState.activeKeep = null;
  }

  clearKeeps() {
    AppState.keeps = [];
  }
}

export const keepsService = new KeepsService();