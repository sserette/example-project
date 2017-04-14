import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { HttpClient } from '../../core/http-client.service';

import { Configuration } from '../../app.constants';

@Injectable()
export class DashboardService {
    private _dashboardUrl: string;

    constructor(
        private _configuration: Configuration,
        private _httpClient: HttpClient
    ) {
        this._dashboardUrl = _configuration.ApiUrl + '/dashboard';
    }

    private handleError(error: any) {
        console.error(error);
        return Observable.throw(error._body || 'Server error');
    }
}