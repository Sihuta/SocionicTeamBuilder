export class Employee {
    constructor(
        public id: number,
        public enterprise: string,
        public login: string,
        public password: string,
        public email: string,
        public role: string,
        public fullName: string,
        public socionicType: string,
        public workingProfile: string
    ) {}
}