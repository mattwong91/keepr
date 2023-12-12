import { RepoItem } from "./RepoItem";

export class Keep extends RepoItem {
  constructor(data) {
    super(data)
    this.creatorId = data.creatorId
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.views = data.views
    this.kept = data.kept
    this.creator = data.creator
    this.vaultKeepId = data.vaultKeepId
  }
}