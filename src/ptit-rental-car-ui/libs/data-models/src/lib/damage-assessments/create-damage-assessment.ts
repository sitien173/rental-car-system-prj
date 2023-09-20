export interface CreateDamageAssessment {
    assessmentDate: Date;
    damageDescription: string;
    repairCost: number;
    totalCost: number;
    rentalContractId: string;
}
