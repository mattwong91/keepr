import { RepoItem } from "./RepoItem";

export class Vault extends RepoItem {
  constructor(data) {
    super(data)
    this.creatorId = data.creatorId
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.isPrivate = data.isPrivate
    this.creator = data.creator
  }
}