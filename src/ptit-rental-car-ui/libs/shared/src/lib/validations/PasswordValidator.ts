import {AbstractControl, ValidationErrors} from "@angular/forms";

export class PasswordValidator {
  private static readonly passwordPatterns: any = {
    hasUpperCase: /[A-Z]+/,
    hasLowerCase: /[a-z]+/,
    hasNumeric: /[0-9]+/,
    hasSpecialCharacters: /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/,
    hasMinimumLength: /.{8,}/,
  };

  static validate(control: AbstractControl): ValidationErrors | null {
    const value = control.value;

    if (!value) {
      return null;
    }

    const isValid = Object.keys(PasswordValidator.passwordPatterns)
      .every((key) => PasswordValidator.passwordPatterns[key].test(value));

    return isValid ? null : {password: true};
  }
}

