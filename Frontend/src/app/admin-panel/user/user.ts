export class User {
    constructor(
        public id: number,
        public login: string,
        public password: string,
        public email: string,
        public role: string,
        public enterpriseId: number
    ) {}
}