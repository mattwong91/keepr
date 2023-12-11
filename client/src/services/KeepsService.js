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
}

export const keepsService = new KeepsService();