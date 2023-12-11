import { RepoItem } from "./RepoItem";

export class Profile extends RepoItem {
  constructor(data) {
    super(data)
    this.name = data.name
    this.picture = data.picture
    this.coverImg = data.coverImg
  }
}