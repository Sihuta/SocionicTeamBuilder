export class TeamFullInfo {
    constructor(
        public teamId: number,
        public employeeIdList: number[],
        public wayOfBuilding: string,
        public isTestedOnPractice: boolean
    ) {}
}