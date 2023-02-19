import { HttpErrorResponse } from "@angular/common/http";
import { throwError } from "rxjs";

export class ErrorHandler {
    private static errorMessage: string = "";

    public static handleError(errorResponse: HttpErrorResponse) {
        if (errorResponse.error instanceof ErrorEvent) {
            //console.error('Client Side Error :', errorResponse.error.message);
        } else {
            //console.error('Server Side Error :', errorResponse);
            this.errorMessage = errorResponse.error;
        }

        return throwError(() => new Error(this.errorMessage));
    }
}