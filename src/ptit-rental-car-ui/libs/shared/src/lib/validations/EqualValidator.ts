import {AbstractControl, ValidatorFn} from "@angular/forms";

export class EqualValidator {
  static validate(controlNameToMatch: string): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const controlToMatch = control.root.get(controlNameToMatch);
      return controlToMatch && control.value !== controlToMatch.value ? {mismatch: true} : null;
    };
  }
}

