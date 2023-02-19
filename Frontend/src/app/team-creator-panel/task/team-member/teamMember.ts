export class TeamMember {
    constructor(
        public id: number,
        public employeeId: number,
        public teamId: number,
        public taskId: number,
        public position: string
    ) {}
}