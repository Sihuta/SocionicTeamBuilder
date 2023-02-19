export class Feedback {
    constructor(
        public id: number,
        public teamMemberId: number,
        public dateTime: Date,
        public mood: string,
        public details: string
    ) {}
}