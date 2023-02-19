import { CreatedTeam } from "./createdTeam";

export class CreatedGroupOfTeams {
    constructor(
        public count: number,
        public descriptionCode: string,
        public createdTeams: CreatedTeam[]
    ) {}
}