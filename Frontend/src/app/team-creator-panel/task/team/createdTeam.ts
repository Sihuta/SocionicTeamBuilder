export class CreatedTeam {
    constructor(
        public count: number,
        public wayOfBuilding: string,
        public category: string,
        public employeeIdList: number[]
    ) {}
}