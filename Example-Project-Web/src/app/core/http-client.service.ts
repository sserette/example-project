import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { BusyService } from './busy.service';

@Injectable()
export class HttpClient {
    constructor(
        private http: Http,
        private busyService: BusyService
    ){}

    get(url: string) {
        let headers = new Headers();
        this.busyService.setBusy();
        return this.http.get(url, {
            headers: headers
        }).finally(() => this.busyService.setNotBusy());
    }

    post(url: string, data: any) {
        let headers = new Headers();
        this.busyService.setBusy();
        return this.http.post(url, data, {
            headers: headers
        }).finally(() => this.busyService.setNotBusy());
    }

    put(url: string, data: any) {
        let headers = new Headers();
        this.busyService.setBusy();
        return this.http.put(url, data, {
            headers: headers
        }).finally(() => this.busyService.setNotBusy());
    }

    delete(url: string) {
        let headers = new Headers();
        this.busyService.setBusy();
        return this.http.delete(url, {
            headers: headers
        }).finally(() => this.busyService.setNotBusy());
    }
}