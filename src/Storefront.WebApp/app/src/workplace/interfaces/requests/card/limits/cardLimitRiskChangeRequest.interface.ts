export interface CardLimitRiskChangeRequestInterface {
  clientId?: number;
  cardId?: number;
  limit?: number;
  isP2PLimited?: boolean;
  limitSetDate?: Date;
  phone?: string;
}