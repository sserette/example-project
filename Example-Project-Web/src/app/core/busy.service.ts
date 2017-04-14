import { Injectable, EventEmitter } from '@angular/core';

@Injectable()
export class BusyService {
    public busyEvent: EventEmitter<boolean>;
    private count: number = 0;
    constructor() {
        this.busyEvent = new EventEmitter<boolean>();
    }

    public setBusy() {
        if(this.count === 0){
            this.busyEvent.emit(true);
        }
        this.count++;
    }

    public setNotBusy() {
        this.count--;
        if(this.count === 0){
            this.busyEvent.emit(false);
        }
    }
}