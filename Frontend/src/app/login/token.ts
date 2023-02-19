export class Token {
    constructor(
        public accessToken: string,
        public expiration: Date,
    ) {}
}