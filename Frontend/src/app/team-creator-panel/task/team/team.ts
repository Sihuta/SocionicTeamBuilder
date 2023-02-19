export class Team {
    constructor(
        public id: number,
        public wayOfBuilding: string,
        public isApproved: boolean,
        public isTestedOnPractice: boolean
    ) {}
}