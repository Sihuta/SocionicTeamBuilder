export class Task {
    constructor(
        public id: number,
        public title: string,
        public employeeCount: number,
        public teamCount: number,
        public minTeamSize: number,
        public timeIsLimited: boolean,
        public teamsCreated: boolean
    ) {}
}