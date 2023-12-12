import { RepoItem } from "./RepoItem";

export class VaultKeep extends RepoItem {
  constructor(data) {
    super(data)
    this.creatorId = data.creatorId
    this.vaultId = data.vaultId
    this.keepId = data.keepId
  }
}