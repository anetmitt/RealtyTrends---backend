WITH filtered_properties AS (
    SELECT p."Id", rp."RegionId", ps."PropertyFieldId", pf."Name", ps."Value", pu."SnapshotId", s."CreatedAt"
    FROM public."Properties" p
             JOIN public."RegionProperties" rp ON p."Id" = rp."PropertyId"
             JOIN public."PropertyStructures" ps ON p."Id" = ps."PropertyId"
             JOIN public."PropertyFields" pf ON ps."PropertyFieldId" = pf."Id"
             JOIN public."PropertyUpdates" pu ON p."Id" = pu."PropertyId"
             JOIN public."Snapshots" s ON pu."SnapshotId" = s."Id"
    WHERE ps."Value" IS NOT NULL
      AND ps."Value" != ''
    AND rp."TransactionTypeId" = CAST(@TransactionType AS UUID)
    AND p."PropertyTypeId" = CAST(@PropertyType AS UUID)
    AND (
    (@CountySelect IS NOT NULL AND rp."RegionId" = CAST(@CountySelect AS UUID))
    OR (@ParishSelect IS NOT NULL AND rp."RegionId" = CAST(@ParishSelect AS UUID))
    OR (@CitySelect IS NOT NULL AND rp."RegionId" = CAST(@CitySelect AS UUID))
    OR (@StreetSelect IS NOT NULL AND rp."RegionId" = CAST(@StreetSelect AS UUID))
    )
    AND s."CreatedAt" BETWEEN @StartDate AND @EndDate
    AND ps."AddedTime" <= s."CreatedAt"
    )

SELECT
    AVG(CASE WHEN pf."Name" = 'Price Per Unit' THEN CAST(fp."Value" AS DOUBLE PRECISION) END) AS "AveragePricePerUnit",
    fp."CreatedAt" AS "SnapshotDate"
FROM filtered_properties fp
         JOIN public."PropertyFields" pf ON fp."PropertyFieldId" = pf."Id"
WHERE (pf."Name" = 'Price Per Unit' OR pf."Name" = 'Rooms' OR pf."Name" = 'Area'
  AND (
        ((pf."Name" = 'Rooms' AND @RoomsCountMin IS NULL AND @RoomsCountMax IS NULL)
             OR (pf."Name" = 'Rooms' AND @RoomsCountMin IS NULL
                     AND CAST(fp."Value" AS DOUBLE PRECISION) <= CAST(@RoomsCountMax AS DOUBLE PRECISION))
             OR (pf."Name" = 'Rooms' AND CAST(fp."Value" AS DOUBLE PRECISION) >= CAST(@RoomsCountMin AS DOUBLE PRECISION)
                     AND CAST(fp."Value" AS DOUBLE PRECISION) <= CAST(@RoomsCountMax AS DOUBLE PRECISION))
            OR (pf."Name" = 'Rooms' AND CAST(fp."Value" AS DOUBLE PRECISION) >= CAST(@RoomsCountMin AS DOUBLE PRECISION)
                AND @RoomsCountMax IS NULL))
            
        OR
        ((pf."Name" = 'Price Per Unit' AND @PricePerUnitMin IS NULL AND @PricePerUnitMax IS NULL)
             OR (pf."Name" = 'Price Per Unit' AND CAST(fp."Value" AS DOUBLE PRECISION) >= CAST(@PricePerUnitMin AS DOUBLE PRECISION) AND CAST(fp."Value" AS DOUBLE PRECISION) <= CAST(@PricePerUnitMax AS DOUBLE PRECISION))
            OR (pf."Name" = 'Price Per Unit' AND @PricePerUnitMin IS NULL AND CAST(fp."Value" AS DOUBLE PRECISION) <= CAST(@PricePerUnitMax AS DOUBLE PRECISION))
            OR (pf."Name" = 'Price Per Unit' AND CAST(fp."Value" AS DOUBLE PRECISION) >= CAST(@PricePerUnitMin AS DOUBLE PRECISION) AND @PricePerUnitMax IS NULL))
        OR
        
        ((pf."Name" = 'Area' AND @AreaMin IS NULL AND @AreaMax IS NULL)
            OR (pf."Name" = 'Area' AND @AreaMin IS NULL AND CAST(fp."Value" AS DOUBLE PRECISION) <= CAST(@AreaMax AS DOUBLE PRECISION))
            OR (pf."Name" = 'Area' AND CAST(fp."Value" AS DOUBLE PRECISION) >= CAST(@AreaMin AS DOUBLE PRECISION)
                     AND CAST(fp."Value" AS DOUBLE PRECISION) <= CAST(@AreaMax AS DOUBLE PRECISION))
            OR (pf."Name" = 'Area' AND CAST(fp."Value" AS DOUBLE PRECISION) >= CAST(@AreaMin AS DOUBLE PRECISION)
                AND @AreaMax IS NULL))
        )
        
            
    )
GROUP BY fp."CreatedAt"
ORDER BY fp."CreatedAt" ASC;