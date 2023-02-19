export class IotData {
    constructor(
        public id: number,
        public teamMemberId: number,
        public dateTime: Date,
        public heartBeat: number,
        public bodyTemperature: number,
        public pulse: number,
        public isFixedOne: boolean,
        public isCritical: boolean
    ) {}
}